import inprogress_img from "../assets/inprogress.png"
import "../css/inprogress.css"



export const InProgress = () => {
    return <div className="inprogress-container">
        <audio src="src\assets\inprogress.mp3" controls autoPlay loop/>
        <div>
            <img src={inprogress_img} alt="inprogress" />
        </div>
    </div>
}