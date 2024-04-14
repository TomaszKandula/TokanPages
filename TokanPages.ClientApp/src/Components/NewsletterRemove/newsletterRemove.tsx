import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { NewsletterRemoveAction } from "../../Store/Actions";
import { ContentUnsubscribeState } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { ContentDto } from "../../Api/Models";
import { NewsletterRemoveView } from "./View/newsletterRemoveView";

interface Properties extends ContentUnsubscribeState {
    id: string;
}

export const NewsletterRemove = (props: Properties): JSX.Element => {
    const contentPre: ContentDto = props.content?.contentPre;
    const contentPost: ContentDto = props.content?.contentPost;

    const dispatch = useDispatch();
    const remove = useSelector((state: ApplicationState) => state.newsletterRemove);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = remove?.status === OperationStatus.notStarted;
    const hasFinished = remove?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [isRemoved, setIsRemoved] = React.useState(false);
    const [hasButton, setHasButton] = React.useState(true);
    const [hasProgress, setHasProgress] = React.useState(false);

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setIsRemoved(false);
        setHasButton(true);
        setHasProgress(false);
    }, [hasProgress, contentPost]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(NewsletterRemoveAction.remove({ id: props.id }));
            return;
        }

        if (hasFinished) {
            setIsRemoved(true);
            setHasButton(false);
            setHasProgress(false);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished]);

    const buttonHandler = React.useCallback(() => {
        if (props.id == null) return;
        setHasProgress(true);
    }, [props.id]);

    return (
        <NewsletterRemoveView
            isLoading={props.isLoading}
            contentPre={contentPre}
            contentPost={contentPost}
            buttonHandler={buttonHandler}
            buttonState={hasButton}
            progress={hasProgress}
            isRemoved={isRemoved}
        />
    );
};
