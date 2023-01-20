using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Utils : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            SceneManager.activeSceneChanged += (a, b) =>
            {
                Time.timeScale = 1;
                _timeSlowed = false;
                _dontResumeTime = false;
            };
        }

        public static void DisableRagdoll(GameObject gameObject)
        {
            foreach (var rb in gameObject.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = true;
            }
        }

        public static void EnableRagdoll(GameObject gameObject)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            foreach (var rb in gameObject.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
            }
        }

        private static bool _timeSlowed;
        private static bool _dontResumeTime;
        public static IEnumerator SlowTime(float timeFlow, float duration)
        {
            yield break;
            if (_timeSlowed)
                _dontResumeTime = true;

            Time.timeScale = timeFlow;
            _timeSlowed = true;
            yield return new WaitForSeconds(duration);
            if (_dontResumeTime)
            {
                _dontResumeTime = false;
                yield break;
            }

            while (Time.timeScale < 1)
            {
                Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.unscaledDeltaTime);
                yield return null;
            }
            Time.timeScale = 1;

            _timeSlowed = false;
        }

        public static Vector3 GetRandomHorizontalVector(float randomRange)
        {
            return new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));
        }

        public static void SetWeaponStats(Weapon weapon)
        {
            weapon.reloadTime -= GameData.Instance.WeaponStats.GetReloadTimeToReduce();
            weapon.spread -= GameData.Instance.WeaponStats.GetSpreadToReduce();
        }
    }
}
