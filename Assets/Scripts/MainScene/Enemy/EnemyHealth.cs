using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    private Animator anim;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;
    private ParticleSystem deadParticles;
    private CapsuleCollider capsuleCollider;
    private EnemyController enemyController;
    private bool isDead;
    private bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = transform.Find("HitParticles").GetComponent<ParticleSystem>();
        deadParticles = transform.Find("DeathParticles").GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        enemyController = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyController>();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();

        deadParticles.Play();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        ScoreManager.num++;
        enemyController.RemoveEnemy(gameObject);
        Destroy (gameObject, 2f);
    }
}
