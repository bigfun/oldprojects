package ShipsClient;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;


class GridArea extends JPanel {

    protected int area[][];
    protected boolean vertical = false;
    private String title;
    protected int size;
    private Point selected;
    protected Point cursorLocation;
    private Rectangle gridRects[][];
    protected PlayingField mainHandle;
    public static final int SHIP_HIT = 1;
    public static final int SHIP_MISS = 2;
    public GridArea(String title, PlayingField mainHandle, int size) {
        area = new int[size][size];
        this.title = title;
        this.size = size;
        this.mainHandle = mainHandle;
        gridRects = new Rectangle[size][size];
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                gridRects[x][y] = new Rectangle(x * 25, y * 25, 25, 25);
            }
        }

        addMouseMotionListener(new MouseMovingHandler());
        addMouseListener(new MouseHandler());

        setOpaque(false);
    }
    public void setWholeArea(int[][] area)
    {
        this.area = area;
    }
    public Point getSelected() {
        Point temp = selected;
        selected = null;
        //mainHandle.selectedShip = 0;		//be sure to get the ship before getSelected
        return temp;
    }

    @Override
    public Dimension getPreferredSize() {
        return new Dimension((int)(25.1*size), 27 *size);
    }

    public void setArea(Point where, int contents) {
        area[(int) where.getX()][(int) where.getY()] = contents;
    }

    public int getArea(Point check) {
        return area[(int) check.getX()][(int) check.getY()];
    }
    public void clearArea()
    {
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                area[x][y] = 0;
            }
        }
    }
    protected boolean validPlacement() {
        if (cursorLocation == null)
            return false;
        if (vertical ) {
            if ((int) cursorLocation.getY() + mainHandle.selectedShipSize > size) {
                return false;
            }
            for (int i = 0; i < mainHandle.selectedShipSize; i++) {
                if (false) {
                    return false;		//if overlapping
                }
            }
        } else {
            if ((int) cursorLocation.getX() + mainHandle.selectedShipSize > size) {
                return false;
            }
            for (int i = 0; i < mainHandle.selectedShipSize; i++) {
                if (false) {
                    return false;		//if overlapping
                }
            }
        }
        return true;
    }

    public void paintComponent(Graphics g) {
        Graphics2D g2 = (Graphics2D) g;

        GradientPaint gp = new GradientPaint(0.0f, 0.0f, new Color(40, 200, 140),
                25.0f * size, 25.0f * size, new Color(40, 180, 210));
        g2.setPaint(gp);
        g2.fillRect(0, 0, size * 25, size * 25);

        g2.setColor(new Color(0, 100, 90));
        for (int i = 1; i < size; i++) {
            g2.drawLine(i * 25, 0, i * 25, size * 25);
            g2.drawLine(0, i * 25, size * 25, i * 25);
        }
        g2.setColor(Color.black);
        g2.draw3DRect(0, 0, size * 25, size * 25, false);

        g2.setColor(new Color(0, 60, 60));
        //g2.setFont(PlayingField.gameFont);		//loading the font is slow
        g2.drawString(title, 125 - (title.length() * 4), 25 * size + 18);
    }
    public boolean isVertical()
    {
        return vertical;
    }
    private class MouseMovingHandler extends MouseMotionAdapter {

        private Rectangle lastSelected = new Rectangle();

        public void mouseMoved(MouseEvent e) {
            int x = (int) (e.getPoint().getX() / 25);
            int y = (int) (e.getPoint().getY() / 25);

            if (x < size && y < size && gridRects[x][y] != lastSelected) {
                lastSelected = gridRects[x][y];
                cursorLocation = new Point(x, y);
                repaint();
            }

        }
    }
    private class MouseHandler extends MouseAdapter {

        public void mousePressed(MouseEvent e) {
            if (e.getModifiers() == e.BUTTON1_MASK) {
                selected = cursorLocation;
                //mainHandle.addChatMessage("You selected: " + selected);	//delete this
            }
            if (e.getModifiers() == e.BUTTON3_MASK) {
                vertical = !vertical;		//toggles vertical ship placing state
                repaint();
            }
        }
    }

}
