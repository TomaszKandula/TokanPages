import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import ArticleList from "../Components/Articles/articleList";
import ArticleDetail from "../Components/Articles/articleDetail";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function ArticlesPage() 
{

    let queryParam = useQuery();
    let id = queryParam.get("id");

    const content = id ? <ArticleDetail uid={id} /> : <ArticleList />;

    return (
        <>
            <Navigation content={null} />
            <Container>
                {content}
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
