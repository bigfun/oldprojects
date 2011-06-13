<?php

if (!$user)
	echo 'Witaj, nieznajomy. '.html::anchor("admin/login", "Zaloguj się").' lub '.html::anchor("admin/register", "Zarejestruj").'.';
else
	echo 'Witaj, '.$user->name.'. '.html::anchor("admin/profile","Twój Profil").' | '.html::anchor("admin/logout", "Wyloguj");

