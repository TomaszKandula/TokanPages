import { ContentActivateAccountState } from "../../States";

export const ContentActivateAccount: ContentActivateAccountState = 
{
    isLoading: false,
    content: 
    {
        language: "",
		onProcessing:
		{
        	type: "Unset",
			caption: "",
        	text1: "", 
        	text2: "",
        	button: ""
		},
		onSuccess: 
		{
        	type: "Unset",
        	caption: "",
        	text1: "", 
        	text2: "",
        	button: ""
		},
		onError:
		{
        	type: "Unset",
        	caption: "",
        	text1: "", 
        	text2: "",
        	button: ""
		}
    }
}
