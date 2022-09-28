import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { GetNavigationContentAction } from "../../Store/Actions";
import { GetFooterContentAction } from "../../Store/Actions";
import { GetPolicyContentAction } from "../../Store/Actions";
import { Navigation } from "../../Components/Layout";
import { Footer } from "../../Components/Layout";
import { DocumentContent } from "../../Components/Document";

export const PolicyPage = (): JSX.Element => 
{
    const dispatch = useDispatch();

    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const policy = useSelector((state: IApplicationState) => state.getPolicyContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.getNavigationContent());
        dispatch(GetFooterContentAction.getFooterContent());
        dispatch(GetPolicyContentAction.getPolicyContent());
    }, 
    [ dispatch, language?.id ]);

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
