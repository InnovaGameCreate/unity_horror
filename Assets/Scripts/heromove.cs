using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(BoxCollider))]

public class heromove : MonoBehaviour
{

    public float speed = 0.1f;
    public float jump = 100;
    public float gravity = -50;
    public Camera my_camera;
    public float runbuttondelay = 30;
    public float normalspeed = 0.1f;
    public float runspeed = 0.2f;

    private Animator anime;
    private Rigidbody body;
    private bool is_ground = true;
    private float wallx = 0;
    private float wallz = 0;
    private float face = 1;

    private float runcount = 0;
    private int rundir=0;

    private RaycastHit wall;
    private enum RunButton
    {
        Firstpush,
        Firstpushover,
        Pull,
        Pullover,
        Secondpush,
        None
    }
    private enum State
    {
        Normal,
        Damaged,
        Invincible,
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

        if (this.state != State.Damaged)
        {
            this.Move(Input.GetAxis("Horizontal"), 0/*Input.GetAxis("Vertical")*/, Input.GetButtonDown("Jump"));
            this.runMove(Input.GetAxis("Horizontal"));
         
        }
    }

    void Move(float x, float z, bool j)
    {
        //向きを設定
        this.face = x > 0 ? -1 : (x < 0 ? 1 : face);
        this.transform.rotation = Quaternion.Euler(0, (this.face + 1) * 90, this.transform.rotation.z);
        this.anime.SetFloat("Horizontal", x != 0 ? x : (z != 0 ? this.face : 0));

        //進行方向の壁を調べる
        if (Mathf.Abs(x) > 0)
        {   //前方
            if (Physics.Raycast(this.transform.position, this.face > 0 ? Vector3.left : Vector3.right,out wall, RAY_LENGTH))
            {
                if (wall.collider.tag != "Enemy")
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
        p = this.transform.position = new Vector3(p.x + x * this.speed, p.y, p.z + z * this.speed);
        p.z = p.z > 5 ? 5 : (p.z < 0 ? 0 : p.z);    //Z=0以上かつ5未満の範囲でZ軸移動できる

        //カメラの位置を設定
        this.transform.position = p;
        this.my_camera.transform.position = new Vector3(p.x, p.y + 2, this.my_camera.transform.position.z);

        //アニメーション設定
        this.anime.SetFloat("Vertical", this.body.velocity.y);
        this.anime.SetBool("isGround", this.is_ground);

        //ジャンプ判定
        if (j&& this.is_ground)
        {
            this.anime.SetTrigger("Jump");
            this.body.AddForce(Vector3.up * this.jump);
            this.is_ground = false;
        }
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

  public void upMove(float y,float under)
    {

        //ユニティちゃんを移動
        Vector3 p = this.transform.position;
        p = this.transform.position = new Vector3(p.x, p.y + y * 0.2f, p.z);
        p.y = p.y < under ? under : p.y;    //Z=0以上かつ5未満の範囲でZ軸移動できる
                                                    //カメラの位置を設定
        this.transform.position = p;
    
    }

    void OnCollisionEnter(Collision collision)
    {
        
   
        
        if (!this.is_ground )
        {
            //衝突したのが地形(Terrain)だったら接地したと判断
            this.is_ground = true;
            this.anime.SetBool("isGround", this.is_ground);
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ladder")&& (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.DownArrow)))
        {
            upMove(Input.GetAxis("Vertical"), other.transform.position.y- other.transform.localScale.y+1.5f);
            GetComponent<Rigidbody>().constraints = (RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY);
          
        }

   
    }

    void OnTriggerExit(Collider other)
    {
        GetComponent<Rigidbody>().constraints = (RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ);
      
    }
    void OnFinishedInvincibleMode()
    {
        this.state = State.Normal;
    }


    

}

