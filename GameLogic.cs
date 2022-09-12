
namespace Server{
    class GameLogic{
        public static float deltaTime { get; private set; }
        private static DateTime lastTime;
        public static DateTime currentTime { get; private set; }
        public static void Start(){
            currentTime = DateTime.Now;
            lastTime = currentTime;
        }
        public static void Update(){
            
            currentTime = DateTime.Now;
            deltaTime = (currentTime.Ticks - lastTime.Ticks) / 10000000f;
            ThreadManager.UpdateMain();
            lastTime = currentTime;
        }
    }
}