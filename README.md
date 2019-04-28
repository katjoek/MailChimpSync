# MailChimpSync
Synchronize an Excel contact list with a single MailChimp supporting interests

MailChimpSync was created one goal: synchronize the list of members of our club with MailChimp while making it easy 
to sent mailings to different, pre-defined subsets of members and ensuring only 1 e-mail address is used per person.
This is achieved using the "interest" groups support of MailChimp. So given a single list of all members, it is possible
to create different "interest" groups such as "junior", "senior", "senior competitive", etc...

MailChimpSync currently supports a single Excel worksheet as input. Using a wizard you can create a configuration to be used
while syncing. You'll need a [MailChimp API key](https://mailchimp.com/help/about-api-keys/) to create a working configuration.
Once created, performing the synchronization is as simple as starting the application and hitting F5.

MailChimpSync performs a one-way-sync. The Excel file is the supposed to be the single source of truth.

Note that currently, if a person unsubscribes from the mailing-list, he/she will be re-added on the next synchronization.

# Supporting other types of input data - a note for developers
If you need another source of data instead of Excel, then that might quite easy to accomplish as long as your data can be
is accessed as a [DataSet](https://docs.microsoft.com/en-us/dotnet/api/system.data.dataset?view=netframework-4.8). But you'll
have to create supporting this yourself.
