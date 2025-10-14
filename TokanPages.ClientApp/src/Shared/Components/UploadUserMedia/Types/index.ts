import { ReactChangeEvent } from "../../../../Shared/Types";
import { UserMedia } from "../../../../Shared/Enums";

export interface UploadUserMediaViewProps {
    customHandle?: string;
    buttonState: boolean;
    inputHandler: (event: ReactChangeEvent) => void;
    accepting: string;
    previewImage?: string;
}

export interface UploadUserMediaProps {
    customHandle?: string;
    handle?: string;
    skipDb?: boolean;
    mediaTarget: UserMedia;
    previewImage?: string;
}
