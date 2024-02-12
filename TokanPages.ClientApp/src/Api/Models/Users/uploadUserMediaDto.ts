import { UserMedia } from "../../../Shared/enums";

export interface UploadUserMediaDto {
    userId?: string;
    mediaTarget: UserMedia;
    data: File;
}
