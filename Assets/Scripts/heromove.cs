using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(BoxCollider))]

public class heromove : MonoBehaviour
{
    public sanValueText sanText; //外部のsanValueTexオブジェクトを見えるよう定義

    private float speed;     
               
    public float jump = 100;            //ジャンプ力
    public float gravity = -50;         //重力
    public Camera my_camera;         
    public float runbuttondelay = 30;   //走る操作を認識する間隔
    public float normalspeed = 0.1f;   //歩くスピード
    public float runspeed = 0.2f;      //走るスピード
    public int ray_length = 8;        //SAN値が削られる敵との視認距離
    public LayerMask mask;              //レイキャスト用マスク

    private Animator anime;
    private Rigidbody body;
    private bool is_ground = true;
    private float wallx = 0;
    private float wallz = 0;
    private float face = 1;

    private float runcount = 0;
    private int rundir=0;

    private RaycastHit wall;

    private float freeze = 0;
    public int zoomouttime = 2;
    public float zoomspeed = 0.3f;
    public float zoomin_z = -10;
    public float zoomout_z = -20;

    RaycastHit hit;
    private enum RunButton
    {
        Firstpush,
        Firstpushover,
        Pull,
        Pullover,
        Secondpush,
        None
    }
    public enum State
    {
        Normal,
        Damaged,
        Invincible,
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
    }

    void Update()
    {

        if (this.state != State.Damaged&& this.state != State.Invincible)
        {
            this.Move(Input.GetAxis("Horizontal"), 0/*Input.GetAxis("Vertical")*/, Input.GetButtonDown("Jump"));
            this.runMove(Input.GetAxis("Horizontal"));
            ray_To_Enemy();
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
            if (Physics.Raycast(this.transform.position, this.face > 0 ? Vector3.left : Vector3.right,out wall, RAY_LENGTH))
            {
                if (wall.collider.tag == "Ground")
                {
                    this.wallx = this.wallx == 0 ? this.transform.position.x : this.wallx;  //まだ壁を検出していなければ位置を保存
                    x = 0;  //これ以上は前に進みません
                }
            }
            else
            {
                this.wallx = 0; //壁はなかった
            }
        }
        if (Mathf.Abs(z) > 0)
        {   //手前もしくは奥
            if (Physics.Raycast(this.transform.position, z > 0 ? Vector3.forward : Vector3.back, RAY_LENGTH))
            {
                this.wallz = this.wallz == 0 ? transform.position.z : this.wallz;
                z = 0;
            }
            else
            {
                this.wallz = 0;
            }
        }

        //ユニティちゃんを移動
        Vector3 p = this.transform.position;
        p = this.transform.position = new Vector3(p.x + x * this.speed* Time.deltaTime, p.y, p.z);
     
        //カメラの位置を設定
        this.transform.position = p;
        this.my_camera.transform.position = new Vector3(p.x, p.y + 2, this.my_camera.transform.position.z);

        //アニメーション設定
        this.anime.SetFloat("Vertical", this.body.velocity.y);
        this.anime.SetBool("isGround", this.is_ground);

        //ジャンプ判定
        if (j&& this.is_ground&&this.state!=State.Upping )
        {

           
            this.anime.SetTrigger("Jump");
            this.body.velocity = new Vector3(0,0,0);
            this.body.AddForce(Vector3.up * this.jump);
            this.is_ground = false;
        }
    }

    //プレイヤーの敵への目線
    public void ray_To_Enemy()
    {

        Ray ray = new Ray(transform.position, this.face > 0 ? Vector3.left : Vector3.right);
   
        if (Physics.Raycast(ray, out hit, 10.0f, mask)) 
           
        {
            if (hit.collider.tag == "Enemy")
            {
                sanText.san --;
            }
        }
    }
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
    }

 

    //カメラズーム(キャラ移動放置時)
    void cameraZoomOut()
    {
        Vector3 p = this.transform.position;
        float z = (this.my_camera.transform.position.z <= zoomout_z)? zoomout_z : this.my_camera.transform.position.z - zoomspeed;
        this.my_camera.transform.position = new Vector3(p.x, p.y + 2, z);
    }

    //カメラズームアウト(キャラ移動時)
    void cameraZoomIn()
    {
        Vector3 p = this.transform.position;
        float z = (this.my_camera.transform.position.z >= zoomin_z) ? zoomin_z : this.my_camera.transform.position.z + zoomspeed;
        this.my_camera.transform.position = new Vector3(p.x, p.y + 2, z);
    }


  
    void OnTriggerStay(Collider other)
    {
      
        if (other.CompareTag("Enemy")&&this.state!=State.Invincible)
           sanText.san-- ;


    }

    void OnTriggerExit(Collider other)
    {
        GetComponent<Rigidbody>().constraints = (RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ);
      
    }

    public State get_state()
    {
        return this.state;
    }
    public void set_state(State nextstate)
    {
        this.state = nextstate;
    }

    public void InvincibleMode()
    {
        this.state = State.Invincible;
    }
    public void OnFinishedInvincibleMode()
    {
        this.state = State.Normal;
    }


    

}

