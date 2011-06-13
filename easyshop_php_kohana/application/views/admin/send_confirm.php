<?php

echo '<h3>Wysyłanie potwierdzenia rejestracji</h3>';
echo '<p>Jeśli list potwierdzający nie dotarł do Ciebie, podaj swój adres email poniżej i naciśnij "Wyślij", aby przesłać go ponownie. Pamiętaj, aby sprawdzić folder ze spamem w swojej skrzynce pocztowej.</p>';

echo form::open();

echo '<p>'.form::label('mail', 'Adres email (użyty podczas rejestracji):').' '.form::input('mail').'<br / ></p>';

echo '<p>'.form::submit('submit', 'Wyślij').'</p>';

echo form::close();
