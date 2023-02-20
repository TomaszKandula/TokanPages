import * as React from "react";

interface BaseProperties
{
    height: number;
    bgcolor: string;
    duration: number;
}

interface Properties extends BaseProperties
{
    width: number;
}

const ProgressStyle = (props: Properties): React.CSSProperties => 
({
    margin: 0,
    padding: 0,
    position: "fixed",
    top: 0,
    zIndex: 99,
    backgroundColor: `${props.bgcolor}`,
    height: `${props.height}px`,
    width: `${props.width}%`,
    transitionProperty: "width",
    transitionDuration: `${props.duration}s`,
    transitionTimingFunction: "ease-out"
});

export const ProgressOnScroll = (props: BaseProperties) => 
{
    const [width, setWidth] = React.useState(0);

    const Scrolling = () => 
    {
        const winScroll: number = document.body.scrollTop || document.documentElement.scrollTop;
        const height: number = document.documentElement.scrollHeight - document.documentElement.clientHeight;
        const scrolled: number = (winScroll / height) * 100;
      
        if (height > 0) 
        {
            setWidth(scrolled);
        } 
        else 
        {
            setWidth(0);
        }
    }

    React.useEffect(() => 
    {
        window.addEventListener("scroll", Scrolling);
        return () => window.removeEventListener("scroll", Scrolling);
    }, []);

    return (
        <div 
            style={ProgressStyle({ width: width, ...props })}>
        </div>
    );
}
