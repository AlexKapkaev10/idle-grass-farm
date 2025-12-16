using UnityEngine.SceneManagement;

namespace Project.Services
{
    public class SceneLoadService : ISceneLoadService
    {
        public void LoadScene(SceneNameType nameType)
        {
            SceneManager.LoadScene(nameType.ToString());
        }
    }
}