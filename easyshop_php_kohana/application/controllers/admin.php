<?php
defined('SYSPATH') or die ('No direct script access.');

class Admin_Controller extends Controller
{
    public function index()
    {
        $this->template->content = $this->_user();
        url::redirect('admin/login');
    }

    public function login()
    {
        $this->template->docutype='form';
        $this->template->formname ='loginform';
        if ($this->_user())
        {
            $this->session->destroy();
        }

        $_POST = Validation::factory($_POST)
        ->add_rules('mail', 'required', 'email')
        ->add_rules('password', 'required');
        if ($_POST->validate())
        {
            $user = ORM::factory('user')->where('mail', $_POST['mail'])->find();

            if ($user->pass == md5($_POST['password']))
            {
                if (!$user->confirmed)
                {
                    $this->session->set_flash('user_message', 'Konto nie potwierdzone. potwierdź konto klikając na link w liście mailowym.');
                    url::redirect('admin/send_confirm');
                }
                else
                {
                    $this->session->set( array (
                         'id'=>$user->id,
                         'permission'=>$user->permission));
                    $this->session->set_flash('user_message', 'Zalogowano!');
                    url::redirect();
                }
            } else
            {
                $this->session->set_flash('user_message', 'Błąd! Zła nazwa użytkownika lub hasło.');
                url::redirect('admin/login');
            }
        } else
        {
            $this->template->title = "easyshop - Logowanie";
            $this->template->content = View::factory('admin/login')->set('errors', $_POST->errors('validation'));
        }
    }
    public function send_confirm()
    {
        if ($this->_user())
        {
            url::redirect();
        }
        else
        {
            $_POST = Validation::factory($_POST)
            ->add_rules('mail', 'required', 'email');
            if ($_POST->validate())
            {
                $user = ORM::factory('user')->where(array('mail' => $_POST['mail'], 'confirmed' => 0))->find();

                if ($user->count_last_query() < 1)
                {
                    $this->session->set_flash('user_message', 'Podany adres email nie istnieje w naszej bazie, lub konto jest potwierdzone.');
                    url::redirect('admin/send_confirm');
                }
                else
                {
                    $this->_send_confirmation(array($_POST["mail"],$user->name.' '.$user->surname), $user->id);
                    $this->session->set_flash('user_message', 'Wiadomość potwierdzająca została wysłana na podany adres email');
                    url::redirect('admin/login');
                }
            } else
            {
                $this->template->title = "easyshop - Potwierdzenie rejestracji";
                $this->template->content = View::factory('admin/send_confirm');
            }
        }

    }

    public function register()
    {
        $this->template->docutype='form';
        $this->template->formname ='registerform';
        if ($this->_user())
        {
            url::redirect();
        }
        else
        {

            $_POST = Validation::factory($_POST)
            ->add_rules('*', 'required')
            ->add_rules('mail', 'required', 'email');
            if ($_POST->validate())
            {
                $user = ORM::factory('user')->where('mail', $_POST['mail'])->find();

                if ($user->count_last_query() > 0)
                {
                    $this->session->set_flash('user_message', 'Podany adres email jest zajęty.');
                    url::redirect('admin/register');
                }
                else
                {
                    $user = ORM::factory('user');
                    $contact = ORM::factory('contact');
                    $user->mail = $_POST["mail"];
                    $user->pass = md5($_POST["password"]);
                    $user->name = $_POST["name"];
                    $user->surname = $_POST["surname"];
                    $user->permission = 1;
                    $user->save();
                    $contact->user_id = $user->id;
                    $contact->city = $_POST["city"];
                    $contact->postcode = $_POST["postcode"];
                    $contact->street = $_POST["street"];
                    $contact->phone = $_POST["phone"];
                    $contact->save();
                    $this->_send_confirmation(array($_POST["mail"],$_POST["name"].' '.$_POST["surname"]), $user->id);
                    $this->session->set_flash('user_message', 'Rejestracja przebiegła pomyślnie.
                     Na podany przez Ciebie adres email wysłana została wiadomość potwierdzająca rejestrację.
                     Po kliknięciu w link potwierdzający zawarty w liście możesz się zalogować.');
                    url::redirect('admin/login');
                }
            } else
            {
                $this->template->title = "easyshop - Rejestracja";
                $this->template->content = View::factory('admin/register')->set( array (
                'errors'=>$_POST->errors('validation')
                    ));
            }
        }
    }

    public function profile($id = -1)
    {
        if ($this->_user() != 'admin' || $id == -1)
        {
            $this->_show_profile($this->session->get('id'));
        }
        else
        $this->_show_profile($id);
    }

    public function _show_profile($id)
    {
        $user = ORM::factory('user')->find($id);
        if ($user->count_last_query() > 0)
        {
            $this->template->title.= ' - Profil: '.$user->name.' '.$user->surname;
            $this->template->content = View::factory('admin/profile')->set('user', $user);
        }
        else
        {
            $this->template->title.= ' - Profil: nie znaleziono';
            $this->template->content = View::factory('admin/notexist');
        }
    }
    public function logout()
    {
        $this->session->destroy();
        $this->session->create();
        $this->session->set_flash('user_message', 'Zostałeś wylogowany!');
        url::redirect();
    }

    public function confirm($id, $token)
    {
        $user = ORM::factory('user')->find($id);
        if ($user->count_last_query() < 1)
        {
            $this->session->set_flash('user_message', 'Błąd - Brak podanego użytkownika w bazie');
            url::redirect();
        }
        if ($user->confirmed)
        {
            $this->session->set_flash('user_message', 'Błąd - Użytkownik już zatwierdzony.');
            url::redirect();
        }
        $confirmation = ORM::factory('confirmation')->where('user_id',$id)->find();
        if ($confirmation->count_last_query() < 1)
        {
            $this->session->set_flash('user_message', 'Błąd - Brak klucza dla podanego użytkownika');
            url::redirect();
        }
        if ($confirmation->token === $token)
        {
            $user->confirmed = 1;
            $user->save($id);
            $this->session->set_flash('user_message', 'Konto potwierdzone. Możesz się zalogować.');
            url::redirect('admin/login');
        }

    }

    private function _generate_confirmation($id)
    {
        $token = text::random('alnum', 25);
        $confirmation = ORM::factory('confirmation');
        $confirmation->user_id = $id;
        $confirmation->token = $token;
        $confirmation->save();
        return $token;
    }

    private function _send_confirmation($to, $id)
    {
        $token = '';
        $query = ORM::factory('confirmation')->where('user_id', $id)->find();
        if ($query->count_last_query() < 1)
        {
            $token = $this->_generate_confirmation($id);
        }
        else
        {
            $token = $query->token;
        }
        $link = url::base().'admin/confirm/'.$id.'/'.$token;
        $from    = 'admin@bigfun.linuxpl.eu';
        $subject = 'easyshop - Potwierdzenie rejestracji';
        $message = 'Witaj, '.$to[1].'<br /><br />';
        $message.= 'Dziękujemy za rejestrację w naszym sklepie.
                    Poniżej znajduje się link, w który musisz kliknąć, aby potwierdzić swoje konto:<br />';
        $message.= html::anchor($link,$link);
        $message.= '<br /><br />';
        $message.= 'Jeśli link nie działa, skopiuj poniższy link i wklej go do przeglądarki:<br />';
        $message.= $link;
        $message.= '<br /><br />Życzymy udanych zakupów,<br />';
        $message.= 'Ekipa easyshop';
        $message.= '<br /><br /><br />Ten email został wygenerowany automatycznie. Prosimy na niego nie odpowiadać.';
        email::send($to, $from, $subject, $message, TRUE);
    }
}
