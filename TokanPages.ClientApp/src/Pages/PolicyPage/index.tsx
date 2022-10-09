import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { DocumentContent } from "../../Components/Document";

import { 
    ContentNavigationAction, 
    ContentFooterAction, 
    ContentPolicyAction 
} from "../../Store/Actions";

export const PolicyPage = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const language = useSelector((state: IApplicationState) => state.applicationLanguage);

    const navigation = useSelector((state: IApplicationState) => state.contentNavigation);
    const footer = useSelector((state: IApplicationState) => state.contentFooter);
    const policy = useSelector((state: IApplicationState) => state.contentPolicy);

    React.useEffect(() => 
    {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentPolicyAction.get());
    }, 
    [ language?.id ]);

    return (
        <>     
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <DocumentContent content={policy?.content} isLoading={policy?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
