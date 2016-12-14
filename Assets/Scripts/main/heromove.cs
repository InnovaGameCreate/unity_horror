using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(BoxCollider))]

public class heromove : MonoBehaviour
{
    public sanValueText sanText; //外部のsanValueTexオブジェクトを見えるよう定義
    public staminaGauge staminaText; //外部のstaminaオブジェクトを見えるよう定義
    public lookenemycount lookText;  //外部のlookenemycountオブジェクトを見えるよう定義
    private bool lookenemy;      //敵からいったん視線を外した状態で敵を見たかどうか

    static public int nowstage;         //現在のステージ

    private float speed;
    public int attacked_power = 15;     //プレイヤーのSAN値減少量           
    public float jump = 100;            //ジャンプ力
    public float gravity = -50;         //重力
    public Camera my_camera;
    public float runbuttondelay = 30;   //走る操作を認識する間隔
    public float normalspeed = 7;   //歩くスピード
    public float runspeed = 14;      //走るスピード
    public int ray_length = 8;        //SAN値が削られる敵との視認距離
    public LayerMask mask;              //レイキャスト用マスク

    private Animator anime;
    private Rigidbody body;
    private bool is_ground = true;
    private float wallx = 0;
    private float wallz = 0;
    private float face = 1;

    private float runcount = 0;
    private int rundir = 0;

    private RaycastHit wall;
    private RaycastHit hitwall;
    private float sety;            //着地時のy座標
    private bool jumpjumpflag;

    private float freeze = 0;               //どれくらい止まってるかのカウント
    public int zoomouttime = 2;             //ズームアウトするまでの時間
    public float zoomspeed = 0.3f;          //カメラのズームスピード
    public float zoomin_z = -10;            //ズームの大きさ
    public float zoomout_z = -20;           //ズームアウトの大きさ

    private bool goal = false;
    RaycastHit hit;
    private float Lockcount = 0;               //動けない間のカウント
    private float unlockcount = 0;          //麻痺解除から次の麻痺を食らうまでのカウント
    private bool eventstop;        //イベント発生時の移動不可
    //走るボタン　2連打したかの状態分岐
    private enum RunButton
    {
        Firstpush,
        Firstpushover,
        Pull,
        Pullover,
        Secondpush,
        None
    }

    //プレイヤーの状態
    public enum State
    {
        Normal,
        Damaged,
        Invincible,             //隠れて動けない状態
        InvincibleMove,         //隠れつつ動ける状態
        NotMove,                //幻覚で動けない状態
        Upping

    }


    private State state = State.Normal;
    private RunButton runstate = RunButton.None;

    private const float RAY_LENGTH = 1.0f;
    private const string TERRAIN_NAME = "Terrain";



    void Start()
    {
        this.anime = this.GetComponent<Animator>();
        this.body = this.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, this.gravity, 0);

        this.face = -1;

        for (int i = 0; i < playerLife.scenename.Length; i++)
        {
            if (SceneManager.GetSceneByName(playerLife.scenename[i]).isLoaded == true)
            {
                nowstage = i;
                break;
            }
        }
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity.y > 40)
            GetComponent<Rigidbody>().velocity = new Vector3(0, 40, 0);
    }


    void Update()
    {

        if (this.state != State.Damaged && this.state != State.Invincible)
        {
            if (SceneManager.GetSceneByName("main_escmenu").isLoaded == false && Lockcount == 0 && eventstop == false)
            {
                this.Move(Input.GetAxis("Horizontal"), 0/*Input.GetAxis("Vertical")*/, Input.GetButtonDown("Jump"));
                this.runMove(Input.GetAxis("Horizontal"));
            }
            else
            {
                this.Move(0, 0, false);
                this.runMove(0);
            }
            if (goal == false)
                ray_To_Enemy();

            if (unlockcount > 0)
            {
                unlockcount += Time.deltaTime;
                //3秒後にプレイヤーが固定可能になる
                if (unlockcount > 3)
                {
                    unlockcount = 0;
                }
            }
            else if (Lockcount > 0)
            {
                Lockcount += Time.deltaTime;
                //2秒後にプレイヤーの固定が解ける
                if (Lockcount > 2)
                {
                    this.anime.SetBool("refuze", false);
                    Lockcount = 0;
                    unlockcount++;
                }
            }
        }
    }

    //移動
    void Move(float x, float z, bool j)
    {
        if (x != 0)
        {
            freeze = 0;
            cameraZoomIn();
        }
        else
        {
            freeze += Time.deltaTime;
            if (freeze > zoomouttime)
                cameraZoomOut();
        }
        //向きを設定
        this.face = x > 0 ? -1 : (x < 0 ? 1 : face);
        this.transform.rotation = Quaternion.Euler(0, (this.face + 1) * 90, this.transform.rotation.z);
        this.anime.SetFloat("Horizontal", x != 0 ? x : (z != 0 ? this.face : 0));


        //進行方向の壁を調べる
        if (Mathf.Abs(x) > 0)
        {   //前方
            if (Physics.Raycast(this.transform.position, this.face > 0 ? Vector3.left : Vector3.right, out wall, RAY_LENGTH) ||
                Physics.Raycast(this.transform.position, this.face > 0 ? Quaternion.Euler(0f, 0f, 20f) * Vector3.left : Quaternion.Euler(0f, 0f, 20f) * Vector3.right, out wall, RAY_LENGTH) ||
                Physics.Raycast(this.transform.position, this.face > 0 ? Quaternion.Euler(0f, 0f, -20f) * Vector3.left : Quaternion.Euler(0f, 0f, -20f) * Vector3.right, out wall, RAY_LENGTH) ||
                Physics.Raycast(this.transform.position, this.face > 0 ? Quaternion.Euler(0f, 0f, 40f) * Vector3.left : Quaternion.Euler(0f, 0f, 40f) * Vector3.right, out wall, RAY_LENGTH * 1.1f) ||
                Physics.Raycast(this.transform.position, this.face > 0 ? Quaternion.Euler(0f, 0f, -40f) * Vector3.left : Quaternion.Euler(0f, 0f, -40f) * Vector3.right, out wall, RAY_LENGTH * 1.1f) ||
                Physics.Raycast(this.transform.position, this.face > 0 ? Quaternion.Euler(0f, 0f, 60f) * Vector3.left : Quaternion.Euler(0f, 0f, 60f) * Vector3.right, out wall, RAY_LENGTH * 1.5f) ||
                 Physics.Raycast(this.transform.position, this.face > 0 ? Quaternion.Euler(0f, 0f, -60f) * Vector3.left : Quaternion.Euler(0f, 0f, -60f) * Vector3.right, out wall, RAY_LENGTH * 1.5f))
            {
                if (wall.collider.tag == "Ground")
                {
                    this.wallx = this.wallx == 0 ? this.transform.position.x : this.wallx;  //まだ壁を検出していなければ位置を保存
                    //xの移動を0にする
                    x = 0;  //これ以上は前に進みません
                }
            }
            else
            {
                this.wallx = 0; //壁はなかった
            }

        }

        //ユニティちゃんを移動
        Vector3 p = this.transform.position;
        p = this.transform.position = new Vector3(p.x + x * this.speed * Time.deltaTime, p.y, p.z);
        //カメラの位置を設定
        this.transform.position = p;
        this.my_camera.transform.position = new Vector3(p.x, p.y + 2, this.my_camera.transform.position.z);

        //アニメーション設定
        this.anime.SetFloat("Vertical", this.body.velocity.y);
        this.anime.SetBool("isGround", this.is_ground);


        //ジャンプ判定
        if (j && this.is_ground && this.state != State.Upping)
        {
            this.anime.SetTrigger("Jump");
            this.body.velocity = new Vector3(0, 0, 0);
            this.body.AddForce(Vector3.up * this.jump);
            this.is_ground = false;
            sety = GetComponent<Transform>().position.y;
            jumpjumpflag = false;
        }

        //壁ジャンプ
        if (Physics.Raycast(this.transform.position, this.face > 0 ? Vector3.left : Vector3.right, out hitwall, RAY_LENGTH + 1) ||
                Physics.Raycast(this.transform.position, this.face > 0 ? Quaternion.Euler(0f, 0f, 20f) * Vector3.left : Quaternion.Euler(0f, 0f, 20f) * Vector3.right, out hitwall, RAY_LENGTH + 1) ||
                Physics.Raycast(this.transform.position, this.face > 0 ? Quaternion.Euler(0f, 0f, -20f) * Vector3.left : Quaternion.Euler(0f, 0f, -20f) * Vector3.right, out hitwall, RAY_LENGTH + 1))
        {
            if (hitwall.collider.tag == "Ground")
                if (j && this.is_ground == false && sety + 2 < GetComponent<Transform>().position.y && !jumpjumpflag)
                {
                    this.anime.SetBool("doubleup", true);
                    jumpjumpflag = true;
                    this.body.velocity = new Vector3(0, 0, 0);
                    this.body.AddForce(Vector3.up * this.jump * 1.3f);

                }
        }
        else
            this.anime.SetBool("doubleup", false);

    }

    //プレイヤーの敵への目線
    public void ray_To_Enemy()
    {
        Vector3 rawposi = transform.position;
        rawposi.x += 0.5f;
        for (int i = 0; i < 9; i++)
        {
            Debug.DrawRay(rawposi, 15 * (this.face > 0 ? Quaternion.Euler(0f, 0f, -20f + i * 5.0f) * Vector3.left : Quaternion.Euler(0f, 0f, -20f + i * 5.0f) * Vector3.right), Color.red, 0, false);
            Ray ray = new Ray(rawposi, this.face > 0 ? Quaternion.Euler(0f, 0f, -20f + i * 5.0f) * Vector3.left : Quaternion.Euler(0f, 0f, -20f + i * 5.0f) * Vector3.right);
            if (Physics.Raycast(ray, out hit, 4.0f, mask))
            {
                if (hit.collider.tag == "Enemy")
                {
                    sanText.minusbig_san(10);
                }

            }

            if (Physics.Raycast(ray, out hit, 9.0f, mask))
            {
                if (hit.collider.tag == "Enemy")
                {
                    sanText.minus_san((float)attacked_power * Time.deltaTime);


                }

            }

            if (Physics.Raycast(ray, out hit, 14.0f, mask))
            {
                if (hit.collider.tag == "Enemy")
                {
                    sanText.minus_san((float)attacked_power * Time.deltaTime);
                    if (lookenemy == false)
                    {
                        lookenemy = true;
                        lookText.addlookcount();
                    }
                    break;
                }

            }
            if (i==8&&hit.collider == null)
                lookenemy = false;


        }

    }

    //地面の接地関連
    public bool get_is_ground()
    {
        return is_ground;
    }

    public void set_is_ground(bool set)
    {
        is_ground = set;
    }

    //走る
    void runMove(float x)
    {
        if (x != 0 && Input.GetKey(KeyCode.LeftShift) && staminaText.get_stamina() > 0)
        {
            staminaText.minus_stamina(0.8f);
            speed = runspeed;
        }
        else
            speed = normalspeed;
        /* 方向きー2回押しver
        if (runcount>0)
            runcount--;
  
        switch (runstate)
        {
            case RunButton.None:
                speed = normalspeed;
                if (Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.RightArrow))
                {
                    rundir = Input.GetKey(KeyCode.LeftArrow) ? 1 : 2;
                    runcount = runbuttondelay;
                    runstate = RunButton.Firstpush;
                }
              //  Debug.Log("RunButton.None");

                break;
            case RunButton.Firstpush:
                if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    runcount = runbuttondelay;
                    runstate = RunButton.Pull;
                    break;
                }
                if (runcount <= 0)
                    runstate = RunButton.Firstpushover;
                //Debug.Log("RunButton.Firstpush");
                break;
            case RunButton.Firstpushover:
                if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    runstate = RunButton.None;
                    rundir = 0;
                }
               // Debug.Log("RunButton.Firstpushover");
                break;
            case RunButton.Pull:
                if ((Input.GetKey(KeyCode.LeftArrow)&&rundir==1) || (Input.GetKey(KeyCode.RightArrow)&&rundir==2))
                {
                    runstate = RunButton.Secondpush;
                    break;
                }
                if (runcount <= 0)
                    runstate = RunButton.Pullover;
                //Debug.Log(" RunButton.Pull");
                break;
            case RunButton.Pullover:
                runstate = RunButton.None;
               // Debug.Log("RunButton.Pullove");
                break;
            case RunButton.Secondpush:
                speed = runspeed;
                if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    runstate = RunButton.None;
                    rundir = 0;
                }
                //Debug.Log(" RunButton.Secondpush");
                break;

   
        }
         */
    }



    //カメラズーム(キャラ移動放置時)
    void cameraZoomOut()
    {
        Vector3 p = this.transform.position;
        float z = (this.my_camera.transform.position.z <= zoomout_z) ? zoomout_z : this.my_camera.transform.position.z - zoomspeed;
        this.my_camera.transform.position = new Vector3(p.x, p.y + 2, z);
    }

    //カメラズームアウト(キャラ移動時)
    void cameraZoomIn()
    {
        Vector3 p = this.transform.position;
        float z = (this.my_camera.transform.position.z >= zoomin_z) ? zoomin_z : this.my_camera.transform.position.z + zoomspeed;
        this.my_camera.transform.position = new Vector3(p.x, p.y + 2, z);
    }

    public void attacked(int minus)
    {
        sanText.minus_san(minus);

    }

    void OnTriggerStay(Collider other)
    {
        //敵と接触時
        if (other.CompareTag("Enemy") && this.state != State.Invincible)
        {
            sanText.minus_san(attacked_power * Time.deltaTime / 3 * 2);
            if (other.gameObject.GetComponent<enemyBase>().lockplayer == true && Lockcount == 0 && unlockcount == 0)
            {
                Lockcount++;
                this.anime.SetBool("refuze", true);
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        ////はしご系から離れた時の物理処理の初期化
        //GetComponent<Rigidbody>().constraints = (RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ);

    }


    //状態の取得
    public State get_state()
    {
        return this.state;
    }
    //状態の設定
    public void set_state(State nextstate)
    {
        this.state = nextstate;
    }
    //隠れた状態に移行
    public void InvincibleMode()
    {
        this.state = State.Invincible;
    }
    //普通の状態に移行
    public void OnFinishedInvincibleMode()
    {
        this.state = State.Normal;
    }
    //隠れて動ける状態に移行
    public void InvincibleMoveMode()
    {
        this.state = State.InvincibleMove;
    }


    //プレイヤーの向き(反転)の取得
    public float get_face()
    {
        return this.face;
    }

    public void set_eventstop(bool next)
    {
        eventstop = next;
    }


    public void set_exlock()
    {
        Lockcount = 0.1f;
        goal = true;
    }

}

