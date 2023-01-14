import { UserMedia } from "../../../Shared/enums";

export interface IUploadUserMedia
{
    userId?: string; 
    mediaTarget: UserMedia; 
    data: File;
}
