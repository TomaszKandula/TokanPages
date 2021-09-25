export interface IActivateAccountContentDto
{
    content: 
    {
		onProcessing:
		{
        	type: string;
			caption: string,
        	text1: string, 
        	text2: string,
        	button: string
		},
		onSuccess: 
		{
        	type: string;
        	caption: string,
        	text1: string, 
        	text2: string,
        	button: string
		},
		onError: 
		{
        	type: string;
        	caption: string,
        	text1: string, 
        	text2: string,
        	button: string
		}
    };
}