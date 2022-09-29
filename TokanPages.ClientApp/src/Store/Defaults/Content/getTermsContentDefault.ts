import { IGetTermsContent } from "../../States/Content/getTermsContentState";

export const GetTermsContentDefault: IGetTermsContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        items: []
    }
}
