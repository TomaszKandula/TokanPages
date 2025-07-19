import * as React from "react";
import { UserNoteResultDto } from "../../../../../Api/Models";
import { Backdrop, Skeleton, TextArea } from "../../../../../Shared/Components";
import { ReactChangeTextEvent } from "../../../../../Shared/types";
import Validate from "validate.js";
import "./userNotesView.css";

interface UserNotesViewProps {
    isLoading: boolean;
    mediaQuery: {
        isTablet: boolean;
        isMobile: boolean;
    };
    hasProgress: boolean;
    userNotes?: UserNoteProps[];
    captionText: string;
    descriptionText: string;
    listLabel: string;
    noteLabel: string;
    onRowClick: (index: number) => void;
    selection?: UserNoteResultDto;
    clearButtonText: string;
    clearButtonHandler: () => void;
    removeButtonText: string;
    removeButtonHandler: () => void;
    saveButtonText: string;
    saveButtonHandler: () => void;
    messageForm: { note: string };
    messageHandler: (event: ReactChangeTextEvent) => void;
    background?: React.CSSProperties;
}

interface UserNoteProps {
    id: string;
    note: string;
    createdBy: string;
    createdAt: string;
    modifiedBy?: string;
    modifiedAt?: string;
}

interface RenderRowProps {
    id: string;
    note: string;
    index: number;
    selection?: string;
    onClick: (index: number) => void | undefined;
}

const RenderRow = (props: RenderRowProps): React.ReactElement => {
    const baseClass = "is-size-6 has-text-grey py-4 px-4 user-notes-text-selection";
    const highlightClass = props.selection === props.id ? "user-notes-highlight-row" : "";

    return (
        <div className={highlightClass} key={props.id}>
            <p className={baseClass} onClick={() => props.onClick(props.index)}>
                {props.note}
            </p>
        </div>
    );
};

export const UserNotesView = (props: UserNotesViewProps): React.ReactElement => {
    const isMobileOrTablet = props.mediaQuery.isMobile || props.mediaQuery.isTablet;
    const hasNotes = props.userNotes && props.userNotes.length > 0;
    const noteUid = props.selection ? ` (${props.selection.id.substring(0, 8)}):` : ":";
    const isEmpty = Validate.isEmpty(props.messageForm.note);
    const flexDirection = isMobileOrTablet ? "is-flex-direction-column" : "is-flex-direction-row";
    const modifiers = "is-align-items-flex-start is-justify-content-left";

    return (
        <section>
            <Backdrop isLoading={props.hasProgress} />
            <div className="bulma-container bulma-is-max-desktop">
                <div className="py-6">
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <Skeleton isLoading={props.isLoading} mode="Text" height={14}>
                                <p className="is-size-4 has-text-grey">{props.captionText}</p>
                            </Skeleton>
                            <hr />
                            <div className="py-4">
                                <Skeleton isLoading={props.isLoading} mode="Text" height={14}>
                                    <p className="is-size-6 has-text-grey pb-5">{props.descriptionText}</p>
                                </Skeleton>
                                <div className={`bulma-cell is-flex ${flexDirection} ${modifiers}`}>
                                    <Skeleton isLoading={props.isLoading} mode="Rect" height={300}>
                                        <div className="user-notes-list-box">
                                            <p className="is-size-6 has-text-grey">{`${props.listLabel}:`}</p>
                                            <div className="user-notes-fixed-list">
                                                {!props.isLoading &&
                                                    props.userNotes?.map((value: UserNoteProps, index: number) => (
                                                        <RenderRow
                                                            key={value.id}
                                                            id={value.id}
                                                            note={value.id.substring(0, 8)}
                                                            index={index}
                                                            selection={props.selection?.id}
                                                            onClick={props.onRowClick}
                                                        />
                                                    ))}
                                            </div>
                                        </div>
                                        <div className="user-notes-message-box">
                                            <p className="is-size-6 has-text-grey">{`${props.noteLabel}${noteUid}`}</p>
                                            <TextArea
                                                required
                                                isFixedSize
                                                uuid="note"
                                                rows={16}
                                                onChange={props.messageHandler}
                                                value={props.messageForm.note}
                                                isDisabled={props.isLoading}
                                                className="user-notes-text-box"
                                            />
                                        </div>
                                    </Skeleton>
                                </div>
                                <div className="bulma-content pt-4">
                                    <Skeleton isLoading={props.isLoading} mode="Rect">
                                        <button
                                            type="submit"
                                            className="bulma-button bulma-is-danger bulma-is-light bulma-is-fullwidth my-4"
                                            disabled={props.isLoading || !hasNotes || !props.selection}
                                            onClick={props.removeButtonHandler}
                                        >
                                            {props.removeButtonText}
                                        </button>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} mode="Rect">
                                        <button
                                            type="submit"
                                            className="bulma-button bulma-is-light bulma-is-fullwidth my-4"
                                            disabled={props.isLoading}
                                            onClick={props.clearButtonHandler}
                                        >
                                            {props.clearButtonText}
                                        </button>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} mode="Rect">
                                        <button
                                            type="submit"
                                            className="bulma-button bulma-is-light bulma-is-link bulma-is-fullwidth my-4"
                                            disabled={props.isLoading || isEmpty}
                                            onClick={props.saveButtonHandler}
                                        >
                                            {props.saveButtonText}
                                        </button>
                                    </Skeleton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
