import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { UserMediaUploadAction } from "../../../Store/Actions";
import { IApplicationState } from "../../../Store/Configuration";
import { RECEIVED_ERROR_MESSAGE } from "../../constants";
import { OperationStatus, UserMedia } from "../../enums";
import { UploadUserMediaView } from "./View/uploadUserMediaView";

interface IUploadUserMedia 
{
    handle?: string;
    skipDb?: boolean;
    mediaTarget: UserMedia;
}

const GetAcceptedType = (media: UserMedia): string => 
{   
    const target = media.toString();

    if (target.includes("image")) { return "image/*"; }
    if (target.includes("video")) { return "video/*"; }

    return "";
}

export const UploadUserMedia = (props: IUploadUserMedia): JSX.Element => 
{
    const dispatch = useDispatch();
    const accepting = GetAcceptedType(props.mediaTarget);

    const store = useSelector((state: IApplicationState) => state.userDataStore.userData);
    const upload = useSelector((state: IApplicationState) => state.userMediaUpload);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const [isUploading, setIsUploading] = React.useState(false);
    const [fileData, setFileData] = React.useState<File | undefined>();

    const hasNotStarted = upload?.status === OperationStatus.notStarted;
    const hasFinished = upload?.status === OperationStatus.hasFinished;
    const hasFile = isUploading && fileData !== undefined;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const clear = React.useCallback(() => 
    {
        if (!isUploading) return;

        dispatch(UserMediaUploadAction.clear());
        setIsUploading(false);
        setFileData(undefined);
    }, 
    [ isUploading ]);

    React.useEffect(() => 
    {
        if (hasError)
        {
            clear();
            return;
        }

        if (hasNotStarted && hasFile) 
        {
            dispatch(UserMediaUploadAction.upload({
                userId: store.userId,
                mediaTarget: props.mediaTarget,
                data: fileData
            }, props.skipDb, props.handle));

            return;
        }

        if (hasFinished) 
        {
            clear();
        }
    }, 
    [ hasError, hasNotStarted, hasFile, hasFinished ]);

    const inputHandler = (event: React.ChangeEvent<HTMLInputElement>) =>
    {
        setFileData(event.target.files ? event.target.files[0] : undefined);
        setIsUploading(true);
    };

    return(<UploadUserMediaView
        buttonState={!isUploading}
        inputHandler={inputHandler}
        accepting={accepting}
    />);
}
