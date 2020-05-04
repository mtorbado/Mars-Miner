
/// <summary>
/// Robot movenent interface. Defines all move actions available to the robot as methods
/// </summary>
public interface IRobotMovement
{
    /// <summary>
    /// Moves the robot one tile foward, based on the robot orientation
    /// </summary>
    void MoveFoward();

    /// <summary>
    /// Moves the robot one tile backward, based on the robot orientation
    /// </summary>
    void MoveBackward();

    /// <summary>
    /// Rotates the robot 90º to its left
    /// </summary>
    void TurnLeft();

    /// <summary>
    /// Rotates the robot 90º to its right
    /// </summary>
    void TurnRight();

    /// <summary>
    /// Does nothing
    /// </summary>
    void Stop();
}