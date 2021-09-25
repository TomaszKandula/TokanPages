import { IGetActivateAccountContent } from "../../States/Content/getActivateAccountContentState";

export const GetActivateAccountContentDefault: IGetActivateAccountContent = 
{
    isLoading: false,
    content: 
    {
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
