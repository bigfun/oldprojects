package ShipsClient;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.border.*;

class PlayingField extends JPanel {
    // Contants for the ships

    public static final int SPLASH = 0,
            CARRIER = 1,
            CRUISER = 2,
            BATTLESHIP = 3,
            SUBMARINE = 4,
            PATROL = 5,
            IDLE = 0,
            SHIP_PLACEMENT = 1,
            POINT_SELECTION = 2;
    public static final String[] SHIP_NAMES = {"Lotniskowiec", "Krążownik", "Niszczyciel", "Łódź podwodna", "Kuter"};
    private boolean ship_placed[] = new boolean[5];
    private int mode = IDLE;
    public int selectedShip = 0;
    public int selectedShipSize = 0;
    static Image target, logo, fire, splash;
    static Image[][] ships = new Image[6][3];	//0 based indices not used
    private int[] hits = new int[6];
    private JPanel fields;
    private GridArea enemyOcean, myOcean;
    //static Font gameFont = new Font("Courier New", Font.BOLD, 14);	//Very slow
    //This should be deleted

    public PlayingField(String gameTitle) {
        setLayout(new BorderLayout());
        ships[CARRIER][1] = (new ImageIcon("carrier.gif")).getImage();
        ships[CRUISER][1] = (new ImageIcon("seawolf.gif")).getImage();
        ships[BATTLESHIP][1] = (new ImageIcon("battleship.gif")).getImage();
        ships[SUBMARINE][1] = (new ImageIcon("submarine.gif")).getImage();
        ships[PATROL][1] = (new ImageIcon("patrol.gif")).getImage();
        ships[CARRIER][2] = (new ImageIcon("carrierv.gif")).getImage();
        ships[CRUISER][2] = (new ImageIcon("seawolfv.gif")).getImage();
        ships[BATTLESHIP][2] = (new ImageIcon("battleshipv.gif")).getImage();
        ships[SUBMARINE][2] = (new ImageIcon("submarinev.gif")).getImage();
        ships[PATROL][2] = (new ImageIcon("patrolv.gif")).getImage();
        splash = (new ImageIcon("splash.gif")).getImage();
        fire = (new ImageIcon("fire.gif")).getImage();
        target = (new ImageIcon("target.gif")).getImage();
        logo = (new ImageIcon("logo.jpg")).getImage();


        fields = new JPanel();
        enemyOcean = new EnemyField(this, 10);
        myOcean = new UserField(this, 10);
        myOcean.addMouseListener(new ClickHandler());
        fields.setLayout(new FlowLayout());
        fields.add(enemyOcean);
        fields.add(new LogoPanel());
        fields.add(myOcean);
        fields.setBorder(new TitledBorder(gameTitle));

        add(fields, BorderLayout.CENTER);
        selectedShip = CARRIER;
        selectedShipSize = 5;
    }

    public void setSelectedShipSize(int size) {
        selectedShipSize = size;
    }

    public void setSelectedShip(int ship) {
        selectedShip = ship;
    }

    public int getMode() {
        return mode;
    }

    public Point getClicked() {
        return enemyOcean.getSelected();
    }

    public int getResult(Point coords) {
        return enemyOcean.getArea(coords);
    }

    public void setResult(Point coordinates, int result) {
        int temp = enemyOcean.getArea(coordinates);
        enemyOcean.setArea(coordinates, result + temp);
        enemyOcean.repaint();
    }

    public void setEnemyArea(int[][] area) {
        enemyOcean.setWholeArea(area);
    }

    public Point getPoint() {
        Point thePoint = null;

        mode = POINT_SELECTION;
        do {
            thePoint = enemyOcean.getSelected();
            try {
                Thread.sleep(10);
            } catch (InterruptedException ie) {
                ie.printStackTrace();
            }
        } while (thePoint == null);

        mode = IDLE;
        return thePoint;
    }
    public boolean areAllflooded()
    {
        if (hits[CARRIER] > 4 && hits[BATTLESHIP] > 3 && hits[CRUISER] > 3 && hits[SUBMARINE] > 2 && hits[PATROL] > 1)
            return true;
        else
            return false;
    }
    public int getHit(Point coordinates) {
        int theArea = myOcean.getArea(coordinates);
        int ship = (theArea / 10) % 10;
        int ret = 0;
        if (ship > 0) {
            hits[ship]++;
            ret = 1;
            switch (ship) {
                case CARRIER:
                    if (hits[ship] > 4) {
                        ret = 2;
                    }
                    break;
                case BATTLESHIP:
                    if (hits[ship] > 3) {
                        ret = 2;
                    }
                    break;
                case SUBMARINE:
                    if (hits[ship] > 2) {
                        ret = 2;
                    }
                    break;
                case CRUISER:
                    if (hits[ship] > 3) {
                        ret = 2;
                    }

                    break;
                case PATROL:
                    if (hits[ship] > 1) {
                        ret = 2;
                    }
                    break;
            }
        }
        myOcean.setArea(coordinates, theArea + ((ship > 0) ? 1 : 2));
        myOcean.repaint();
        return ret;
    }

    public void placeShips() {
        mode = SHIP_PLACEMENT;
        validate();
    }

    private void placeShip(Point selectedPoint) {
        if (selectedShipSize > 0 && selectedShip > 0 && ship_placed[selectedShip - 1] == false) {
            myOcean.setArea(selectedPoint, selectedShip * 10 + (myOcean.isVertical() ? 2 : 1) * 100);
            for (int i = 1; i < selectedShipSize; i++)
            {
                if (myOcean.isVertical())
                {
                    myOcean.setArea(new Point((int)selectedPoint.getX(),(int)selectedPoint.getY()+i), selectedShip * 10);
                }
                else
                {
                    myOcean.setArea(new Point((int)selectedPoint.getX()+i,(int)selectedPoint.getY()), selectedShip * 10);
                }
            }
            ship_placed[selectedShip - 1] = true;
            selectedShipSize = 0;
            selectedShip = 0;
        }
    }

    public void clearPlacements() {
        myOcean.clearArea();
        int i = 0;
        while (i++ < 4) {
            ship_placed[i] = false;
        }
        myOcean.repaint();
    }

    public boolean areAllPlaced() {
        boolean allPlaced = false;
        int i = 0;
        while (i++ < 4) {
            allPlaced = true;
            allPlaced = allPlaced && ship_placed[i];
        }
        return allPlaced;
    }

    public EnemyField getEnemyField() {
        return (EnemyField) enemyOcean;
    }

    private class ClickHandler extends MouseAdapter {

        @Override
        public void mousePressed(MouseEvent e) {
            if (e.getModifiers() == MouseEvent.BUTTON1_MASK) {
                if (e.getSource() instanceof UserField) {
                    UserField field = (UserField) e.getSource();
                    if (field.validPlacement() && selectedShipSize > 0 && selectedShip > 0);
                    {
                        placeShip(field.getSelected());
                    }

                }
            }
        }
    }
}
