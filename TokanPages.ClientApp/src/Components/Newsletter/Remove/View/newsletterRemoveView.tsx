import * as React from "react";
import { ContentDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { CustomCard, Icon, ProgressBar } from "../../../../Shared/Components";
import { ExtendedViewProps } from "../newsletterRemove";

interface NewsletterRemoveViewProps extends ViewProperties, ExtendedViewProps {
    isMobile: boolean;
    hasEmptyId: boolean;
    contentPre: ContentDto;
    contentPost: ContentDto;
    buttonHandler: () => void;
    buttonState: boolean;
    progress: boolean;
    isRemoved: boolean;
}

const ActiveButton = (props: NewsletterRemoveViewProps): React.ReactElement => {
    const content: ContentDto = props.isRemoved ? props.contentPost : props.contentPre;
    return (
        <button
            type="submit"
            onClick={props.buttonHandler}
            className="bulma-button bulma-is-light bulma-is-fullwidth"
            disabled={props.progress || !props.buttonState || props.hasEmptyId}
        >
            {!props.progress ? content.button : <ProgressBar size={20} />}
        </button>
    );
};

export const NewsletterRemoveView = (props: NewsletterRemoveViewProps): React.ReactElement => {
    const content: ContentDto = props.isRemoved ? props.contentPost : props.contentPre;
    return (
        <section className={props.background}>
            <div className={`bulma-container bulma-is-max-desktop ${props.isMobile ? "px-4" : ""}`}>
                <div className={!props.className ? "py-6" : props.className}>
                    <CustomCard
                        isLoading={props.isLoading}
                        caption={content.caption}
                        text={[content.text1, content.text2, content.text3]}
                        icon={<Icon size={3} name="Email" />}
                        colour="has-text-info"
                        externalButton={<ActiveButton {...props} />}
                    />
                </div>
            </div>
        </section>
    );
};
