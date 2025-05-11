import * as React from "react";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import EmailIcon from "@material-ui/icons/Email";
import { ContentDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { CustomCard } from "../../../../Shared/Components";
import { ExtendedViewProps } from "../newsletterRemove";

interface NewsletterRemoveViewProps extends ViewProperties, ExtendedViewProps {
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
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            className={props.buttonState ? "button" : ""}
            disabled={props.progress || !props.buttonState || props.hasEmptyId}
        >
            {!props.progress ? content.button : <CircularProgress size={20} />}
        </Button>
    );
};

export const NewsletterRemoveView = (props: NewsletterRemoveViewProps): React.ReactElement => {
    const content: ContentDto = props.isRemoved ? props.contentPost : props.contentPre;
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-wide">
                <div className={!props.className ? "pb-120" : props.className}>
                    <CustomCard
                        isLoading={props.isLoading}
                        caption={content.caption}
                        text={[content.text1, content.text2, content.text3]}
                        icon={<EmailIcon />}
                        colour="info"
                        externalButton={<ActiveButton {...props} />}
                    />
                </div>
            </Container>
        </section>
    );
};
