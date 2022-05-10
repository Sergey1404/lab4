namespace lab4 {
    public class Caretaker {
        private object memento;
        public void SaveState(IOriginator originator) {
            memento = originator.GetMemento();
        }
        public void RestoreState(IOriginator originator) {
            originator.SetMemento(memento);
        }
    }
}
