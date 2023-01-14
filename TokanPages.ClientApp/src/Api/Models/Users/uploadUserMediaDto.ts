import { UserMedia } from "../../../Shared/enums";

export interface IUploadUserMediaDto
{
    userId?: string; 
    mediaTarget: UserMedia; 
    data: File;
}
