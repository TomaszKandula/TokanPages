import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { DocumentContent } from "../../Components/Document";

import { ContentNavigationAction, ContentFooterAction, ContentPolicyAction } from "../../Store/Actions";

export const PolicyPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    const navigation = useSelector((state: ApplicationState) => state.contentNavigation);
    const footer = useSelector((state: ApplicationState) => state.contentFooter);
    const policy = useSelector((state: ApplicationState) => state.contentPolicy);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentPolicyAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <DocumentContent content={policy?.content} isLoading={policy?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
};
