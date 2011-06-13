/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ShipsClient;

import java.awt.*;

class EnemyField extends GridArea {

    public EnemyField(PlayingField handle, int size) {
        super("Plansza przeciwnika", handle, size);
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
                    switch (current) {
                        case GridArea.SHIP_HIT: {
                            g2.drawImage(PlayingField.fire, 25 * x, 25 * y, this);
                            break;
                        }
                        case GridArea.SHIP_MISS: {
                            g2.drawImage(PlayingField.splash, 25 * x, 25 * y, this);
                            break;
                        }
                        default:
                            break;
                    }
                }
            }
        }
        if (cursorLocation != null) {
            g2.drawImage(PlayingField.target, 25 * (int) cursorLocation.getX(), 25 * (int) cursorLocation.getY(), this);
        }
    }
}
