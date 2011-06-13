<?php

echo '<h2 class="formtitle">Rejestracja</h2>';

if (!empty($errors)) {
    echo '<ul>';
    foreach ($errors as $error) {
        echo '<li>'.$error.'</li>';
    }
    echo '</ul>';
}
echo form::open(NULL,array('id' =>'registerForm', 'class' => 'easyform'));
echo form::open_fieldset();
echo form::legend('Podaj swoje dane (wszystkie pola są wymagane):');
echo '<p>'.form::label('mail', 'Adres email (używany do logowania):').' '.form::input('mail').'</p>';
echo '<p>'.form::label('password', 'Hasło:').' '.form::password('password').'</p>';
echo '<p>'.form::label('confirm_password', 'Powtórz hasło:').' '.form::password('confirm_password').'</p>';
echo '<p>'.form::label('name', 'Imię:').' '.form::input('name').'</p>';
echo '<p>'.form::label('surname', 'Nazwisko:').' '.form::input('surname').'</p>';
echo '<p>'.form::label('street','Ulica, nr domu/nr mieszkania:').' '.form::input('street').'</p>';
echo '<p>'.form::label('city','Miasto:').' '.form::input('city').'</p>';
echo '<p>'.form::label('postcode','Kod pocztowy:').' '.form::input('postcode').'</p>';
echo '<p>'.form::label('phone','Telefon (stacjonarny lub komórkowy):').' '.form::input('phone').'</p>';

echo '<p>'.form::submit('submit', 'Zarejestruj', ' class="submit"').'</p>';
echo form::close_fieldset();
echo form::close();
