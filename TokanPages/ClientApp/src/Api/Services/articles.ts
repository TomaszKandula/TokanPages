import ApiRequest from "../../Api/apiRequest";
import { IAlertModal } from "../../Shared/Modals/alertDialog";
import { IUpdateArticleDto } from "../../Api/Models";
import { API_COMMAND_UPDATE_ARTICLE } from "../../Shared/constants";
import { GetUpdateArticleError, GetUpdateArticleSuccess } from "Shared/Modals/messageHelper";

export const UpdateArticle = async (PayLoad: IUpdateArticleDto): Promise<IAlertModal> =>
{
    const apiRequest = ApiRequest(
    { 
        method: "POST", 
        url: API_COMMAND_UPDATE_ARTICLE, 
        data: 
        { 
            id: PayLoad.id,
            title: PayLoad.title,
            description: PayLoad.description,
            textToUpload: PayLoad.textToUpload,
            imageToUpload: PayLoad.imageToUpload,
            isPublished: PayLoad.isPublished,
            addToLikes: PayLoad.addToLikes,
            upReadCount: PayLoad.upReadCount
        }
    });

    const results = await apiRequest;

    if (results.isSucceeded)
    {
        return { 
            State: true, 
            Titile: "Update Article", 
            Message: GetUpdateArticleSuccess(), 
            Icon: 0 
        };
    }
    else
    {
        return { 
            State: true, 
            Titile: "Update Article | Error", 
            Message: GetUpdateArticleError(results.error.ErrorMessage), 
            Icon: 2 
        };
    }
}
