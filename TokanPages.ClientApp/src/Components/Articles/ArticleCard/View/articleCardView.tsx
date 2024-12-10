import * as React from "react";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import { GET_FLAG_URL } from "../../../../Api/Request";
import { RenderImage } from "../../../../Shared/Components";

interface ArticleCardViewProps {
    imageUrl: string;
    title: string;
    description: string;
    onClickEvent: () => void;
    buttonText: string;
    flagImage: string;
    canAnimate: boolean;
}

interface DivAnimatedProps {
    canAnimate: boolean;
    children: React.ReactNode;
}

const Animated = (props: DivAnimatedProps): React.ReactElement => {
    return props.canAnimate ? <div data-aos="fade-up">{props.children}</div> : <>{props.children}</>;
}

export const ArticleCardView = (props: ArticleCardViewProps): React.ReactElement => {
    return (
        <Animated canAnimate={props.canAnimate}>
            <Card elevation={0} className="article-card">
                <CardMedia image={props.imageUrl} className="article-card-image">
                    <RenderImage basePath={GET_FLAG_URL} imageSource={props.flagImage} className="article-flag-image" />
                </CardMedia>
                <CardContent>
                    <Typography className="article-card-title">
                        {props.title}
                    </Typography>
                    <Typography className="article-card-description">{props.description}</Typography>
                    <CardActions className="article-card-action">
                        <Button
                            onClick={props.onClickEvent}
                            size="small"
                            className="button article-button article-card-button"
                        >
                            {props.buttonText}
                        </Button>
                    </CardActions>
                </CardContent>
            </Card>
        </Animated>
    );
};
