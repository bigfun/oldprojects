<?php

echo '<h2 class="formtitle">Logowanie</h2>';

if (!empty($errors)) {
    echo '<ul>';
    foreach ($errors as $error) {
        echo '<li>'.$error.'</li>';
    }
    echo '</ul>';
}

echo form::open(NULL,array('id' =>'loginForm', 'class' => 'easyform'));
echo form::open_fieldset();
echo form::legend('Podaj swój adres email użyty podczas rejestracji oraz hasło:');
echo '<p>'.form::label('mail', 'E-mail:').' '.form::input('mail').'</p>';

echo '<p>'.form::label('password', 'Hasło:').' '.form::password('password').'</p>';

echo '<p>'.form::submit('submit', 'Zaloguj!',  ' class="submit"').'</p>';
echo form::close_fieldset();
echo form::close();
