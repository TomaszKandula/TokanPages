import * as React from "react";
import { IconType } from "../../../enums";
import { RenderParagraphs, RenderList, Icon } from "../../../../Shared/Components";

interface Properties {
    isOpen: boolean;
    icon: IconType | undefined;
    title: string | undefined;
    message: string[] | undefined;
    validation?: object;
    disablePortal?: boolean;
    hideBackdrop?: boolean;
    closeHandler: () => void;
}

const RenderIcon = (props: Properties): React.ReactElement | null => {
    const iconSize = 2.0;
    switch (props.icon) {
        case IconType.info:
            return <Icon name="Information" size={iconSize} className="has-text-info" />;
        case IconType.warning:
            return <Icon name="Alert" size={iconSize} className="has-text-warning" />;
        case IconType.error:
            return <Icon name="AlertCircle" size={iconSize} className="has-text-danger" />;
        default:
            return null;
    }
};

const RenderValidationList = (props: Properties): React.ReactElement => {
    const validation = props.validation;
    const result: string[] = [];
    if (validation) {
        Object.keys(validation).forEach((key, _) => {
            const prop = key as keyof typeof validation;
            const data = validation[prop] as string | string[];

            if (Array.isArray(data)) {
                data.forEach((item: string) => {
                    result.push(item);
                });
            } else {
                result.push(data);
            }
        });
    }

    return <RenderList list={result} className="my-3" />;
};

export const ApplicationDialogBoxView = (props: Properties): React.ReactElement => (
    <div className={`bulma-modal ${props.isOpen ? "bulma-is-active" : ""}`}>
        <div className="bulma-modal-background"></div>
        <div className="bulma-modal-card py-6 my-6">
            <header className="bulma-modal-card-head">
                <RenderIcon {...props} />
                <p className="bulma-modal-card-title p-3">{props.title}</p>
                <button className="bulma-delete" aria-label="close" onClick={props.closeHandler}></button>
            </header>
            <section className="bulma-modal-card-body">
                <div className="bulma-content">
                    <RenderParagraphs
                        className="is-size-6 has-text-dark"
                        text={props.message ?? []}
                        replace={{
                            key: "{LIST}",
                            object: <RenderValidationList {...props} />,
                        }}
                    />
                </div>
            </section>
            <footer className="bulma-modal-card-foot is-justify-content-flex-end">
                <div className="bulma-buttons">
                    <button className="bulma-button bulma-is-link bulma-is-light" onClick={props.closeHandler}>
                        OK
                    </button>
                </div>
            </footer>
        </div>
    </div>
);
