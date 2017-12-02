/*Yebai
 * This script controls the behavior of the GUI guidance arrow */
using UnityEngine;
namespace VRStandardAssets.Utils
{ 
    // This class fades in and out arrows which indicate to
    // the player which direction they should be facing.
	public class VG_RingArrow : MonoBehaviour
    {
		public float angleDelta;

        [SerializeField] private float m_FadeDuration = 0.1f;       // How long it takes for the arrows to appear and disappear.
        [SerializeField] private float m_ShowAngle = 30f;           // How far from the desired facing direction the player must be facing for the arrows to appear.
		[SerializeField] private Transform m_DesiredLocation;      // Indicates which direction the player should be facing (uses world space forward if null).
        [SerializeField] private Transform m_Camera;                // Reference to the camera to determine which way the player is facing.
        [SerializeField] private Renderer[] m_ArrowRenderers;       // Reference to the renderers of the arrows used to fade them in and out.

        private float m_CurrentAlpha;                               // The alpha the arrows currently have.
        private float[] m_TargetAlpha;                              // The alpha the arrows are fading towards.
        private float m_FadeSpeed;                                  // How much the alpha should change per second (calculated from the fade duration).
		private int angleSign;
        private const string k_MaterialPropertyName = "_Alpha";     // The name of the alpha property on the shader being used to fade the arrows.


	    private void Start () {
            // Speed is distance (zero alpha to one alpha) divided by time (duration).
            m_FadeSpeed = 1f / m_FadeDuration;
	    }


        private void Update()
        {	//Set the arrow ring to aling with camera.
			//transform.SetPositionAndRotation(m_Camera.position, m_Camera.rotation);  //stick to head
			if (HiddenGameManager.Instance.catHide ||HiddenGameManager.Instance.holdVG) {//Cat is hiding
				m_TargetAlpha = new float[]{ 0f, 0f };
				for (int i = 0; i < m_ArrowRenderers.Length; i++)
				{
					m_ArrowRenderers[i].material.SetFloat(k_MaterialPropertyName, m_TargetAlpha[i]);
				}
			} else { //Cat is showing
				transform.position = m_Camera.position;
				transform.eulerAngles = new Vector3(0, m_Camera.eulerAngles.y, 0);// Refresh both posistion and rotation.
				Traceing ();
			}

//            // If the difference is greater than the angle at which the arrows are shown, their target alpha is one otherwise it is zero.
//			if (angleDelta < m_ShowAngle && -1 * m_ShowAngle < angleDelta) {
//				m_TargetAlpha = new float[]{0f, 0f, 0f};
//			} else if (angleDelta < -1 * m_ShowAngle) {
//				m_TargetAlpha = new float[]{1f, 0f, 0f};
//			} else if (m_ShowAngle < angleDelta) {
//				m_TargetAlpha = new float[]{0f, 0f, 1f};
//			} else {
//				m_TargetAlpha = new float[]{1f, 1f, 1f};
//			}
//
//            // Increment the current alpha value towards the now chosen target alpha and the calculated speed.
//			for (int i = 0; i < m_ArrowRenderers.Length; i++) {
//				m_CurrentAlpha = Mathf.MoveTowards (m_CurrentAlpha, m_TargetAlpha[i], m_FadeSpeed * Time.deltaTime);
//			}
//				
//
//            // Go through all the arrow renderers and set the given property of their material to the current alpha.
//            for (int i = 0; i < m_ArrowRenderers.Length; i++)
//            {
//
//                m_ArrowRenderers[i].material.SetFloat(k_MaterialPropertyName, m_CurrentAlpha);
//            }
      }

		public void Traceing(){
			Vector3 directAim = m_DesiredLocation.transform.position - m_Camera.transform.position; // The vector shooting from camrea to the cat
			Vector3 desiredForward = m_DesiredLocation == null ? Vector3.forward : Vector3.ProjectOnPlane(directAim, Vector3.up).normalized;//The vector is now flat on x-z plane
			Vector3 flatCamForward = Vector3.ProjectOnPlane(m_Camera.forward, Vector3.up).normalized; // The forward vector of the camera as it would be on a flat plane.

			// The difference angle between the desired facing and the current facing of the player.
			angleSign= Vector3.Cross(desiredForward, flatCamForward).y < 0 ? -1 : 1; //Just -1 or 1
			angleDelta = angleSign*Vector3.Angle(desiredForward, flatCamForward);

			// If the difference is greater than the angle at which the arrows are shown, their target alpha is one otherwise it is zero.
			if (-1 * m_ShowAngle < angleDelta && angleDelta < m_ShowAngle) { //within in -30 to 30
				m_TargetAlpha = new float[]{0f, 0f};
			} else if (angleDelta < -1*m_ShowAngle) { // smaller than -30
				m_TargetAlpha = new float[]{1f, 0f};
			} else if (m_ShowAngle < angleDelta) {   //lager than 30
				m_TargetAlpha = new float[]{0f, 1f};
			} else {
				m_TargetAlpha = new float[]{1f, 1f};
			}
			for (int i = 0; i < m_ArrowRenderers.Length; i++)
			{
				m_ArrowRenderers[i].material.SetFloat(k_MaterialPropertyName, m_TargetAlpha[i]);
			}
		}


		// Turn off the arrows entirely.
        public void Hide()
        {
            gameObject.SetActive(false);
        }


        // Turn the arrows on.
        public void Show ()
        {
            gameObject.SetActive(true);
        }
    }
}