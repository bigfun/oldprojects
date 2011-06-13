

package KoToClient;


public class KotoClient {


    public static KotoView view;


        public static void main(String args[]) {
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                view = new KotoView();
                view.setVisible(true);
            }
        });
    }


    


}
