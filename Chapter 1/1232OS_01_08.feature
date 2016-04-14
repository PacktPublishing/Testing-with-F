Feature: Authentication

Scenario: Entering correct login information makes user authenticated
  Given a fresh browser session at http://mikaellundin.name/login
  When entering 'mikaellundin' as username
  And entering 'hellofsharp' as password
  Then browser should redirect to http://mikaellundin.name/profile