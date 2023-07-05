namespace CsharpWebAPI.Models
{
	public class AddContactRequest
	{
		//the reasoning behind this class is that we do not want to get all of the values from the user, 
		//in my contacts class there is only a name and an ID, but we want to supply the ID, the user does not supply the ID
		//at least when adding a contact, etc.
		public string Name { get; set; }
	}
}
