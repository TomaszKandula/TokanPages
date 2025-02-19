import * as React from "react";
import BackupIcon from "@material-ui/icons/Backup";
import { IconButton } from "@material-ui/core";
import { ReactChangeEvent } from "../../../../Shared/types";
import { UserAvatar } from "../../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface Properties {
    customHandle?: string;
    buttonState: boolean;
    inputHandler: (event: ReactChangeEvent) => void;
    accepting: string;
    previewImage?: string;
}

export const UploadUserMediaView = (props: Properties): React.ReactElement => {
    const uuid = uuidv4();
    const hasPreviewImage = !Validate.isEmpty(props.previewImage);

    return (
        <label htmlFor={props.customHandle ? props.customHandle : `userFile-${uuid}`}>
            <input
                hidden
                accept={props.accepting}
                id={props.customHandle ? props.customHandle : `userFile-${uuid}`}
                name={props.customHandle ? props.customHandle : `userFile-${uuid}`}
                type="file"
                multiple={false}
                onChange={props.inputHandler}
            />

            <IconButton component="span" size="small" disabled={!props.buttonState} className="button-upload">
                {hasPreviewImage ? (
                    <UserAvatar
                        altSource={props.previewImage ?? ""}
                        isLarge={false}
                        className="user-avatar-avatar-button"
                    />
                ) : (
                    <BackupIcon />
                )}
            </IconButton>
        </label>
    );
};
