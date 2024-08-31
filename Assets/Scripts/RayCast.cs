using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour {

    // Methods Vars
    float diagonalTilesNum;
    Vector3 resultantCoordinate;

    Ray ray;
    RaycastHit hit;
    public GameObject selectedPiece;
    public float speed, pieceMovementSpeed;

    public string playerTurn;
    public const string WhitePlayer = "White Piece", 
                        BlackPlayer = "Black Piece";

    void Start() {
        selectedPiece = null;
        playerTurn = WhitePlayer;
    }

    float isDiagonal(Vector3 p0, Vector3 p) {
        resultantCoordinate = new Vector3(Mathf.Abs(p[0] - p0[0]), 0, Mathf.Abs(p[2] - p0[2]));
        
        if(resultantCoordinate[0] == resultantCoordinate[2]) 
            return (resultantCoordinate[0] * (p0[0] > p[0] ? -1 : 1));
            // return (resultantCoordinate[0]);

        return 0;
    }

    GameObject getPieceIn(Vector3 coordinate) {     
        foreach(GameObject piece in GameObject.FindGameObjectsWithTag("Piece"))
            if(piece.transform.position[0] == coordinate[0] && piece.transform.position[2] == coordinate[2]) 
                return piece;

        return null;
    }

    Vector3 unityVector3(Vector3 coordinate) {
        return new Vector3(coordinate[0] >= 0 ? 1 : -1, coordinate[1] >= 0 ? 1 : -1, coordinate[2] >= 0 ? 1 : -1);
    }

    void Update() {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
            if (Input.GetMouseButtonDown(0)) {
                if(selectedPiece != null) {
                    selectedPiece.SendMessage("Unselect");
                    selectedPiece.SendMessage("SetHighPriority", false);
                }

                if(hit.collider.tag == "Piece" && (hit.collider.name == playerTurn || hit.collider.name == "King " + playerTurn)) {
                    selectedPiece = hit.transform.gameObject;
                    selectedPiece.SendMessage("Select");
                } 

                if(hit.collider.name == "Black Tile" && selectedPiece != null) {
                    if(this.getPieceIn(hit.transform.position) == null){

                        diagonalTilesNum = this.isDiagonal(selectedPiece.transform.position, hit.transform.position);
                        if(selectedPiece.name == "Black Piece") diagonalTilesNum *= -1;

                        bool isNormalPossibleMove = diagonalTilesNum == 1,
                             isNormalPossibleEat = Mathf.Abs(diagonalTilesNum) == 2 && getPieceIn(unityVector3(hit.transform.position - selectedPiece.transform.position) + selectedPiece.transform.position).name != selectedPiece.name,
                             isKingPossibleMove = Mathf.Abs(diagonalTilesNum) > 2 && (selectedPiece.name == "King White Piece" || selectedPiece.name == "King Black Piece");
    

                        if(isNormalPossibleMove || isNormalPossibleEat || isKingPossibleMove) {
                            selectedPiece.SendMessage("MoveTo", new Vector3(hit.transform.position[0], selectedPiece.transform.position[1], hit.transform.position[2]));
                            playerTurn = playerTurn == WhitePlayer ? BlackPlayer : WhitePlayer;
                        }

                        selectedPiece.SendMessage("Unselect");
                        selectedPiece = null;
                    }
                }
            }
        }
    }
}
