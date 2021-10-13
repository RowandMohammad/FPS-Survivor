using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public Movement Movement;
  [Header("Movement Types")]
  public float movementSpeed = 40f;
  float rbDrag = 6f;



  float horizontalMove;
  float verticalMove;

  Rigidbody rb;
  
  private void Start() {
    rb = GetComponent<Rigidbody>();
    Movement = new Movement(movementSpeed);
    rb.freezeRotation = true;


  }

  private void Update() {
    playerInput();
    Debug.Log(rb.position);
  }

  void playerInput(){
    horizontalMove = Input.GetAxisRaw("Horizontal");
    verticalMove = Input.GetAxisRaw("Vertical");

    

  }

  void dragControl(){
    rb.drag = rbDrag;
  }


  private void FixedUpdate() {
    PlayerMover();
    

  }

  void PlayerMover(){
    rb.AddForce(Movement.calculate(horizontalMove, verticalMove), ForceMode.Acceleration);

  }


}




  


