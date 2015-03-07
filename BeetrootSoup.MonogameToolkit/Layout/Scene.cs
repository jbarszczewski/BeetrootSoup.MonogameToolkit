namespace BeetrootSoup.MonogameToolkit.Layout
{
    public class Scene : Node
    {
        public Scene(string sceneName)
        {
            this.SceneName = sceneName;
        }

        public string SceneName { get; set; }
    }
}
