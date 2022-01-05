# PaymentDebt

This program system was developed for one organization.
The organization were tired of noticing their partners about debts.
And then they asked me to develop a program that would send notification letters by e-mail.

Notification_Interface is a UI program, where users can monitor, add and delete debt data.
It uses .txt file to read/write data.

Payment_Notification is not a UI. It works on autorun with an operational system.
It reads data from the .txt file used by Notification_Interface and sends by an e-mail smtp port.
Then sets flags about last shipment.
HTML template was used to show beautiful mail message.

Logins and passwords are deleted in the source files.