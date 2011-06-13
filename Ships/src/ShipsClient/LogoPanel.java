/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ShipsClient;
import java.awt.*;
import javax.swing.*;
/**
 *
 * @author bigfun
 */
class LogoPanel extends JPanel
{
	public Dimension getPreferredSize()		{	return new Dimension(100,270);	}
	public void paintComponent(Graphics g)	{	g.drawImage(PlayingField.logo, 0, 0, this);	}
}
