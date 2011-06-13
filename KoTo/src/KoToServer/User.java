

package KoToServer;

import java.util.*;


public class User {

    private int userId;
    private String password;
    private Date lastLoggon;
    private ArrayList messages;


   public User (int userId_, String pass) {
      this.userId = userId_;
      this.password = pass;
      lastLoggon = new Date();
      messages = new ArrayList();
   }

   public static User createUser(int userId_, String pass){
       return new User(userId_, pass);
   }

   public int getId() {
       return this.userId;
   }

   public boolean checkPassword(String pass)
   {
       if (this.password.equals(pass))
           return true;
       else
           return false;
   }
   public boolean checkUser(int userId_, String pass){
       if (userId_ == this.userId && this.password.equals(pass))
           return true;
       else
           return false;

   }

   public void setLastLoggon(){
        this.lastLoggon = Calendar.getInstance().getTime();
   }

   public Date getLastLoggon(){
       return this.lastLoggon;
   }

   public void setLastLoggon(Date date_)
   {
       this.lastLoggon = date_;
   }

   public void addMessage(String message){
       if (this.messages.size() > 9)
           this.messages.remove(0);
       this.messages.add(message);
   }

   public ArrayList getMessages()
   {
       return this.messages;
   }

   public void clearMessages()
   {
       this.messages.clear();
   }

   public boolean hasMessages()
   {
       if (this.messages.size() > 0)
           return true;
       else
           return false;
   }

   public String toString()
   {
       return new String(userId+":"+password);
   }

   public User (int userId_, String pass, ArrayList messages_, Date lastLoggon_ ){
       this.userId = userId_;
       this.password = pass;
       this.messages = messages_;
       this.lastLoggon = lastLoggon_;
   }


}
