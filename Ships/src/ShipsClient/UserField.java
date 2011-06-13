/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ShipsClient;

import java.awt.*;

/**
 *
 * @author bigfun
 */
class UserField extends GridArea {

    public UserField(PlayingField handle, int size) {
        super("Twoja plansza", handle, size);
    }

    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);
        Graphics2D g2 = (Graphics2D) g;

        int current;
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                if (area[x][y] != 0) {
                    current = area[x][y];
                    if (current/100 > 0)
                    {
                        g2.drawImage(PlayingField.ships[(current / 10) % 10][current / 100], 25 * x, 25 * y, this);
                    }
                    if (current%10 == 1)
                    {
                        g2.drawImage(PlayingField.fire, 25 * x, 25 * y, this);
                    }
                    else if (current%10 == 2)
                    {
                        g2.drawImage(PlayingField.splash, 25 * x, 25 * y, this);
                    }
                }
            }
        }
        if (mainHandle.selectedShipSize != 0 && validPlacement()) {
            if (vertical) {
                g2.fill3DRect(25 * (int) cursorLocation.getX(),
                        25 * (int) cursorLocation.getY(), 25, 25 * mainHandle.selectedShipSize, false);
            } else {
                g2.fill3DRect(25 * (int) cursorLocation.getX(),
                        25 * (int) cursorLocation.getY(), 25 * mainHandle.selectedShipSize, 25, false);
            }
        }
    }
}
