namespace apiClinica.Model
{
    public class PacientePlano
    {
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int PlanoSaudeId { get; set; }
        public PlanoSaude PlanoSaude { get; set; }
    }

}
