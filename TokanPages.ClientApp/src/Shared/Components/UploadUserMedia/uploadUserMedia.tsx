import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ReactChangeEvent } from "../../../Shared/Types";
import { UserMediaUploadAction } from "../../../Store/Actions";
import { ApplicationState } from "../../../Store/Configuration";
import { RECEIVED_ERROR_MESSAGE } from "../../constants";
import { OperationStatus, UserMedia } from "../../enums";
import { UploadUserMediaView } from "./View/uploadUserMediaView";

interface Properties {
    customHandle?: string;
    handle?: string;
    skipDb?: boolean;
    mediaTarget: UserMedia;
    previewImage?: string;
}

const GetAcceptedType = (media: UserMedia): string => {
    const target = media.toString();

    if (target.includes("image")) {
        return "image/*";
    }

    if (target.includes("video")) {
        return "video/*";
    }

    return "";
};

export const UploadUserMedia = (props: Properties): React.ReactElement => {
    const dispatch = useDispatch();
    const accepting = GetAcceptedType(props.mediaTarget);

    const upload = useSelector((state: ApplicationState) => state.userMediaUpload);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const [isUploading, setIsUploading] = React.useState(false);
    const [fileData, setFileData] = React.useState<File | undefined>();

    const hasNotStarted = upload?.status === OperationStatus.notStarted;
    const hasFinished = upload?.status === OperationStatus.hasFinished;
    const hasFile = isUploading && fileData !== undefined;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const clear = React.useCallback(() => {
        if (!isUploading) return;

        dispatch(UserMediaUploadAction.clear());
        setIsUploading(false);
        setFileData(undefined);
    }, [isUploading]);

    React.useEffect(() => {
        if (hasError) {
            clear();
            return;
        }

        if (hasNotStarted && hasFile) {
            dispatch(
                UserMediaUploadAction.upload(
                    {
                        binaryData: fileData,
                    },
                    props.skipDb,
                    props.handle
                )
            );

            return;
        }

        if (hasFinished) {
            clear();
        }
    }, [hasError, hasNotStarted, hasFile, hasFinished]);

    const inputHandler = React.useCallback((event: ReactChangeEvent) => {
        setFileData(event.target.files ? event.target.files[0] : undefined);
        setIsUploading(true);
    }, []);

    return (
        <UploadUserMediaView
            customHandle={props.customHandle}
            buttonState={!isUploading}
            inputHandler={inputHandler}
            accepting={accepting}
            previewImage={props.previewImage}
        />
    );
};
