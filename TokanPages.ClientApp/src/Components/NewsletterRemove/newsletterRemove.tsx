import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { NewsletterRemoveAction } from "../../Store/Actions";
import { OperationStatus } from "../../Shared/enums";
import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { ContentDto } from "../../Api/Models";
import { NewsletterRemoveView } from "./View/newsletterRemoveView";
import Validate from "validate.js";

export interface ExtendedViewProps {
    className?: string;
    background?: string;
}

export interface NewsletterRemoveProps extends ExtendedViewProps {
    id: string | null;
}

export const NewsletterRemove = (props: NewsletterRemoveProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const content = data.components.pageNewsletterRemove;

    const contentPre: ContentDto = content.contentPre;
    const contentPost: ContentDto = content.contentPost;

    const dispatch = useDispatch();
    const remove = useSelector((state: ApplicationState) => state.newsletterRemove);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = remove?.status === OperationStatus.notStarted;
    const hasFinished = remove?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [isRemoved, setIsRemoved] = React.useState(false);
    const [hasButton, setHasButton] = React.useState(true);
    const [hasProgress, setHasProgress] = React.useState(false);

    const hasEmptyId = Validate.isEmpty(props.id);

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setIsRemoved(false);
        setHasButton(true);
        setHasProgress(false);
    }, [hasProgress, contentPost]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            dispatch(NewsletterRemoveAction.clear());
            return;
        }

        if (hasNotStarted && hasProgress && props.id) {
            dispatch(NewsletterRemoveAction.remove({ id: props.id }));
            return;
        }

        if (hasFinished) {
            setIsRemoved(true);
            setHasButton(false);
            setHasProgress(false);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, props.id]);

    const buttonHandler = React.useCallback(() => {
        if (props.id == null) return;
        setHasProgress(true);
    }, [props.id]);

    return (
        <NewsletterRemoveView
            isLoading={data.isLoading}
            hasEmptyId={hasEmptyId}
            contentPre={contentPre}
            contentPost={contentPost}
            buttonHandler={buttonHandler}
            buttonState={hasButton}
            progress={hasProgress}
            isRemoved={isRemoved}
            className={props.className}
            background={props.background}
        />
    );
};
