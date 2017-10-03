1. Don't forget to add your public facing Url to the CORS list in your Okta organization, otherwise you will get the following error message:

Response to preflight request doesn't pass access control check: No 'Access-Control-Allow-Origin' header is present on the requested resource. Origin 'https://okta.ngrok.io' is therefore not allowed access.

To do so:
1. Navigate to the Admin dashboard, then select Security => API.
2. From the Tokens tab, select the CORS tab, click Edit in the "CORS Configuration" table, make sure the "Enable CORS for the following base URLs" is selected and enter your public facing url (such as https://okta.ngrok.io)
3. Press the Save button
4. In Settings => Customization, scroll down to the bottom of the page and in the "IFrame Embedding" section, make sure the "Allow IFrame embedding" checkbox is checked