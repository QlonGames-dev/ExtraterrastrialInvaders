using UnityEngine;
using CnControls;

// Just in case so no "duplicate definition" stuff shows up

namespace UnityStandardAssets.Copy._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
		//AudioManager audioManager;

        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


		public AudioClip jump;
		AudioSource audioSource; 

        private void Awake()
        {
			audioSource = GetComponent<AudioSource>();

            m_Character = GetComponent<PlatformerCharacter2D>();
        }
        
		void Start()
		{
			//audioManager = AudioManager.instance;
		}

        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CnInputManager.GetButtonDown("Jump");
				SoundJump ();

            }
        }
        
        private void FixedUpdate()
        {
            float h = CnInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, m_Jump);
            m_Jump = false;
        }
			
		void SoundJump()
		{
			audioSource.PlayOneShot(jump, 0.7F);
		}

    }
}
