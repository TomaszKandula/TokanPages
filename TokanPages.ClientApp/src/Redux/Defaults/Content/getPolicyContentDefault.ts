import { IGetPolicyContent } from "../../../Redux/States/Content/getPolicyContentState";

export const GetPolicyContentDefault: IGetPolicyContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        items: []
    }
}
