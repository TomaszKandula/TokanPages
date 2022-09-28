import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { GetNavigationContentAction } from "../../Store/Actions";
import { GetFooterContentAction } from "../../Store/Actions";
import { GetUpdatePasswordContentAction } from "../../Store/Actions";
import { Navigation } from "../../Components/Layout";
import { Footer } from "../../Components/Layout";
import { UpdatePassword } from "../../Components/Account";

export const UpdatePasswordPage = (): JSX.Element => 
{
    const dispatch = useDispatch();
    
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const updateForm = useSelector((state: IApplicationState) => state.getUpdatePasswordContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.getNavigationContent());
        dispatch(GetFooterContentAction.getFooterContent());
        dispatch(GetUpdatePasswordContentAction.getUpdatePasswordContent());
    }, 
    [ dispatch, language?.id ]);
    
    return (
        <>     
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UpdatePassword content={updateForm?.content} isLoading={updateForm?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
