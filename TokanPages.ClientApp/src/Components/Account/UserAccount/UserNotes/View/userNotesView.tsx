import * as React from "react";
import { UserNoteResultDto } from "../../../../../Api/Models";
import { Backdrop, TextArea } from "../../../../../Shared/Components";
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
    userNotes?: UserNoteProps[] | undefined;
    captionText: string;
    descriptionText: string;
    listLabel: string;
    noteLabel: string;
    onRowClick: (index: number) => void;
    selection?: UserNoteResultDto | undefined;
    clearButtonText: string;
    clearButtonHandler: () => void;
    removeButtonText: string;
    removeButtonHandler: () => void;
    saveButtonText: string;
    saveButtonHandler: () => void;
    messageForm: { note: string };
    messageHandler: (event: ReactChangeTextEvent) => void;
    messageMultiline: boolean;
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
    const highlightClass = props.selection === props.id ? "user-notes-highlight-row" : "";
    return (
        <div className={highlightClass} key={props.id}>
            <p className="is-size-6 has-text-grey py-4 px-4" onClick={() => props.onClick(props.index)}>
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

    return (
        <section>
            <Backdrop isLoading={props.hasProgress} />
            <div className="bulma-container bulma-is-max-desktop">
                <div className="pt-120 pb-40">
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <p className="is-size-4 has-text-grey">
                                {props.captionText}
                            </p>
                            <hr />
                            <div className="py-4">
                                <p className="is-size-6 has-text-grey pb-5">
                                    {props.descriptionText}
                                </p>

                                <div className={`bulma-cell is-flex ${flexDirection} is-align-items-flex-start is-justify-content-left`}>

                                    <div className="user-notes-container">
                                        <p className="is-size-6 has-text-grey">
                                            {`${props.listLabel}:`}
                                        </p>
                                        <div
                                            className={`user-notes-fixed-list user-notes-border mt-10 ${props.isLoading ? "loading-indicator" : ""}`}
                                        >
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
                                        <p className="is-size-6 has-text-grey">
                                            {`${props.noteLabel}${noteUid}`}
                                        </p>
                                        <div className="user-notes-text-box mt-10">
                                            <TextArea
                                                required
                                                fullWidth
                                                isFixedSize
                                                uuid="note"
                                                rows={16}
                                                onChange={props.messageHandler}
                                                value={props.messageForm.note}
                                                isDisabled={props.isLoading}
                                            />
                                        </div>
                                    </div>

                                </div>

                                <div className="bulma-content pt-4">
                                    <button
                                        type="submit"
                                        className="bulma-button bulma-is-danger bulma-is-light bulma-is-fullwidth my-4"
                                        disabled={props.isLoading || !hasNotes || !props.selection}
                                        onClick={props.removeButtonHandler}
                                    >
                                        {props.removeButtonText}
                                    </button>
                                    <button
                                        type="submit"
                                        className="bulma-button bulma-is-light bulma-is-fullwidth my-4"
                                        disabled={props.isLoading}
                                        onClick={props.clearButtonHandler}
                                    >
                                        {props.clearButtonText}
                                    </button>
                                    <button
                                        type="submit"
                                        className="bulma-button bulma-is-light bulma-is-link bulma-is-fullwidth my-4"
                                        disabled={props.isLoading || isEmpty}
                                        onClick={props.saveButtonHandler}
                                    >
                                        {props.saveButtonText}
                                    </button>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
